import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    vus: 10, //numero de usuários virtuais, pode ser passado via comando line
    duration: '30s', //duração do teste
  };


export default function () {
  const res = http.get('http://localhost:5240/api/threads');
  check(res, { 'status was 200': (r) => r.status == 200 });
  sleep(1);
}