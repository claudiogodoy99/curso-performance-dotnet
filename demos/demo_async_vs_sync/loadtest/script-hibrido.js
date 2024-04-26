import http from 'k6/http';
import { check, sleep } from 'k6';

const BASE_URL = 'http://localhost:5222'; // Update with your API base URL

export const options = {
  stages: [
    { duration: '1m', target: 200 }, // Ramp-up to 50 VUs over 1 minute
    { duration: '3m', target: 500 }, // Stay at 50 VUs for 3 minutes
    { duration: '1m', target: 100 }, // 
  ],
  thresholds: {
    http_req_duration: ['p(95)<500'], // 95% of requests must complete within 500ms
  },
};

// Just call Get endpoint
export default function () {
  const resSync = http.get(`${BASE_URL}/WeatherForecast/normal`);
  const resAsync = http.get(`${BASE_URL}/WeatherForecast/async`);
  check(resAsync, { 'status is 200': (r) => r.status === 200 });
  check(resAsync, { 'status is 200': (r) => r.status === 200 });
  
  sleep(1);
}
