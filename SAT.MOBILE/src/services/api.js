import axios from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';

const api = axios.create({
    baseURL: 'https://sat.perto.com.br/SAT.V2.API/api',
    headers: {
        Authorization: `Bearer ${await AsyncStorage.getItem("token")}`,
        Accept: 'application/json',
        'Content-Type': 'application/json'
    }
});

export default api;