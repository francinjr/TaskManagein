import { TaskInterface } from "../interfaces/TaskInterface";
import { apiClient } from "./config";
import { AxiosResponse } from 'axios';

class TaskService {
  findAll(): Promise<AxiosResponse<TaskInterface[]>> {
    return apiClient.get("api/Task");
  }

  findById(id: number): Promise<AxiosResponse<TaskInterface>> {
    return apiClient.get(`api/Task/${id}`);
  }

  create(data: TaskInterface): Promise<AxiosResponse<TaskInterface>> {
    return apiClient.post("api/Task", data);
  }

  update(id: number, data: TaskInterface): Promise<AxiosResponse<TaskInterface>> {
    return apiClient.put(`api/Task/${id}`, data);
  }

  delete(id: number): Promise<AxiosResponse<void>> {
    return apiClient.delete(`api/Task/${id}`);
  }
}

export default new TaskService();
