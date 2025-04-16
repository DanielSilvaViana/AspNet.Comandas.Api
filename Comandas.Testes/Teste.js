import http, { url } from 'k6/http';
import { check, sleep } from 'k6';

export let options =
{
    vus: 150,
    duration: '20s'
};
const urlBase = 'http://localhost:5000';
const token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InN0cmluZyIsIk1pbmhhIENsYWltIjoiT2kiLCJuYW1laWQiOiIyIiwibmJmIjoxNzQ0Njc2NjkyLCJleHAiOjE3NDQ2ODAyOTIsImlhdCI6MTc0NDY3NjY5Mn0.VNi4aLhcgw7g0WCRcC0FYsr8Kqqu1ThOnXqnDNx66-E';
const mesaId = 1;
export default function () {

    const url = `${urlBase}/api/Mesas/${mesaId}`;

    const headers = {

        headers: {

            'Authorization': `Bearer ${token}`,

            'Accept': 'application/json',

        },

    };

    const result = http.get(url, headers);


    check(result, {
        'status:200': (x) => x.status === 200,
        'respostaTemCorpo': (x) => {
            try {
                const data = JSON.parse(x.body);
                return data;
            }
            catch {
                return false;
            }
        }
    });
    sleep(1);
}