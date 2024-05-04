import axios, { type AxiosInstance } from "axios";

export const apiClient: AxiosInstance = axios.create({
  baseURL: "https://localhost:7256/",
  headers: {
    "Content-type": "application/json",
  },
});