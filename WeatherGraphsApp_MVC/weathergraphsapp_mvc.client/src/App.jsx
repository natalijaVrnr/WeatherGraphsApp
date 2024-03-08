import { useEffect, useState } from 'react';
import WeatherChart from './components/WeatherChart';
import './App.css';

function App() {
    const [weather, setWeather] = useState();
    const [latestUpdatedTime, setLatestUpdatedTime] = useState();
    const [isLoading, setIsLoading] = useState(true);
    const [isError, setIsError] = useState(false);

    useEffect(() => {
        fetchWeatherData(setWeather, setIsError);
    }, []);

    useEffect(() => {
        if (weather) {
            getLatestUpdatedTime(weather, setLatestUpdatedTime, setIsError);
            setIsLoading(false);
        }     
    }, [weather]);

    if (isError) {
        return (
            <h1>Oops! An error has occured. Please refresh once the ASP.NET backend has started. If the issue persists, please contact the administrator.</h1>
        )
    }

    return (
        <div>
            <h1 id="tabelLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the OpenWeatherMap API.</p>
            {isLoading ?
            (
                <p><em>Loading weather data...</em></p>
            ):(
                <div>
                    <h3>Latest updated time: {String(latestUpdatedTime)}</h3>
                    <WeatherChart weatherApiData={weather} />
                </div>
            )}            
        </div>
    );
}

const fetchWeatherData = async (setWeather, setIsError) => {
    try {
        const response = await fetch('Weather');
        const data = await response.json();
        setWeather(data);
    } catch (err) {
        console.log('An error has occured: ' + err);
        setIsError(true);
    }
}

const getLatestUpdatedTime = (weather, setLatestUpdatedTime, setIsError) => {
    try {
        const latestTime = weather.reduce((latest, item) => {
            const currentDateTime = new Date(item.createdDateTime);
            return latest < currentDateTime ? currentDateTime : latest;
        }, new Date(weather[0].createdDateTime));
        setLatestUpdatedTime(latestTime);
    } catch (err) {
        console.log('An error has occured: ' + err);
        setIsError(true);
    }
}

export default App;