import PropTypes from 'prop-types';
import {
    Chart as ChartJS,
    BarElement,
    CategoryScale,
    LinearScale,
    Tooltip,
    Legend
} from 'chart.js';

import { Bar } from 'react-chartjs-2';

ChartJS.register(
    BarElement,
    CategoryScale,
    LinearScale,
    Tooltip,
    Legend
);

const WeatherChart = ({ weatherApiData } ) => {
    const cities = [...new Set(weatherApiData.map(item => item.city))];

    const minTemperatures = [];
    const maxTemperatures = [];

    cities.forEach(city => {
        const cityWeather = weatherApiData.filter(item => item.city === city);
        const minTemperature = Math.min(...cityWeather.map(item => item.temperature));
        const maxTemperature = Math.max(...cityWeather.map(item => item.temperature));

        minTemperatures.push([city, minTemperature]);
        maxTemperatures.push([city, maxTemperature]);
    });

    const datasets = [
        {
            label: 'Min Temperature',
            data: minTemperatures.map(item => item[1]),
            backgroundColor: '#80daeb',
            borderColor: 'black',
            borderWidth: 1
        },
        {
            label: 'Max Temperature',
            data: maxTemperatures.map(item => item[1]),
            backgroundColor: '#f08080',
            borderColor: 'black',
            borderWidth: 1
        }
    ];
    const chartData = {
        labels: cities,
        datasets: datasets
    };

    const options = {};

    return (
        <div>
            <Bar data={chartData} options={ options } />
        </div>
    );
};

WeatherChart.propTypes = {
    weatherApiData: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number.isRequired,
            country: PropTypes.string.isRequired,
            city: PropTypes.string.isRequired,
            temperature: PropTypes.number.isRequired,
            createdDateTime: PropTypes.string.isRequired
        })
    ).isRequired
};


export default WeatherChart;

