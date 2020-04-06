import axios from "axios"
import environment from "./environment";

const instance = axios.create({
    baseURL: environment.baseUrl + "/api",
    headers: {
        'Access-Control-Allow-Origin': '*'
    }
});

export default instance
