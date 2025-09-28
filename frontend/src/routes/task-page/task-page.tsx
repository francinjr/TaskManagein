import { useEffect, useState } from "react";
import { TaskInterface } from "../../interfaces/task-interface";
import { NavBar } from "../../components/nav-bar/nav-bar";
import styles from "./styles.module.css";
import { TaskForm } from "../../components/forms/task-form";
import TaskService from "../../services/task-service";
import axios, { AxiosError, AxiosResponse } from "axios";
import { Task } from "../../components/task/task";
import { ApiResponseAlert } from "../../components/commons/api-response-alert/api-response-alert";
import { ApiResponse } from "../../services/api-response";

const defaultTaskFormData: TaskInterface = {
  name: "",
  description: "",
  status: 1,
  id: 0,
};



export const TaskPage = () => {
  useEffect(() => {
    console.log("Componente montado");
    const fetchData = async () => {
      try {
        const response: AxiosResponse<TaskInterface[]> =
          await TaskService.findAll();
        setTasks(response.data);
        // setLoading(false);
      } catch (error) {
        console.error("Error fetching data:", error);
        // setLoading(false);
      }
    };

    fetchData();
  }, []);

  const [tasks, setTasks] = useState<TaskInterface[]>([]);
  //const [loading, setLoading] = useState(true);
  const [taskFormData, setTaskFormData] = useState<TaskInterface>({
    ...defaultTaskFormData,
  });
  const [showModal, setShowModal] = useState(false);

  const [apiMessage, setApiMessage] = useState("");
  const [apiMessadeDuration, setApiMessageDuration] = useState(0);
  const [operationStatus, setOperationStatus] = useState(true);

  const [showApiResponseAlert, setShowApiResponseAlert] = useState(false);

  function handleCompleteTask(id: number) {
    const taskIndex = tasks.findIndex((item) => item.id === id);

    if (taskIndex === -1) {
      return;
    }

    const newTasks: TaskInterface[] = [...tasks];

    if (newTasks[taskIndex].status == 1) {
      newTasks[taskIndex].status = 2;
    } else if (newTasks[taskIndex].status == 2) {
      newTasks[taskIndex].status = 3;
    }

    setTasks(newTasks);
  }

  /*function createTask(): void {
    // chamar a api

    const newTask: TaskInterface = {
      ...taskFormData,
      id: tasks.length + 1,
    };

    setTasks([...tasks, newTask]);

    setShowModal(false);
    setTaskFormData({ ...defaultTaskFormData });
  }*/

  const createTask = async () => {
    let response: AxiosResponse<ApiResponse<TaskInterface>> | null = null;
    let error: any = null;

    try {
      response = await TaskService.create(taskFormData);

      setTasks([...tasks, response.data.data]);
      closeModal();

    } catch (err) {
      error = err;
      console.error("Ocorreu um erro:", error);
    } finally {
      showOperationResult(response, error);
    }
  }


  const showOperationResult = (response: AxiosResponse<ApiResponse<TaskInterface>> | null, error: any) => {
    if(response != null) {
      setOperationStatus(true);
      setApiMessage(response.data.message);
    } else {
      setOperationStatus(false);
      setApiMessage(error.data.message);
    }
    setShowApiResponseAlert(true);
    // Agora tenho que mudar a resposne da api
  }


  const deleteTask = async (taskId: number) => {
    try {
      await TaskService.delete(taskId);

      setTasks(tasks.filter(task => task.id !== taskId));
      closeModal();

      setOperationStatus(true);
      setApiMessage("Tarefa deletada com sucesso!");
      setShowApiResponseAlert(true);

    } catch (error) {
      setOperationStatus(false);
      setApiMessage("Ocorreu um erro ao deletar a tarefa!");
      setShowApiResponseAlert(false);

      console.error("Ocorreu um erro:", error);
    }
  }



  function openModal() {
    setShowModal(true);
  }

  function closeModal() {
    setShowModal(false);
  }

  function showData() {
    console.log(tasks[0].name);
    console.log(tasks[0].id);

    console.log(tasks[1].name);
    console.log(tasks[1].id);

    console.log(tasks[2].name);
    console.log(tasks[2].id);
  }

  return (
    <>
      <div className="mainContainer">
        <div className={`navContainer ${showModal && styles.blur}`}>
          <NavBar />
        </div>

        <div className="centerContainer">
          <div className={`${showModal && styles.blur}`}>
            <main>
              <h1 className="title">TaskManagein</h1>

              <div className="inputGroup">
                <input
                  className="input"
                  placeholder="Pesquisar tarefa"
                  type="text"
                />
                <button className="button" onClick={openModal}>
                  Nova
                </button>
              </div>

              <div className={styles.task}>
                {tasks.length > 0 &&
                  tasks.map((item) => (
                    <Task
                      key={item.id}
                      tasks={item}
                      handleCompleteTask={handleCompleteTask}
                      deleteTask={deleteTask}
                    />
                  ))}

                {!tasks.length && <p>Não há nenhuma tarefa</p>}
              </div>
              <ApiResponseAlert
                operationStatus={operationStatus}
                message={apiMessage}
                messageDuration={5000}
                showApiResponseAlert={showApiResponseAlert}
                setShowApiResponseAlert={setShowApiResponseAlert}
              />
            </main>
          </div>
          <TaskForm
            taskFormData={taskFormData}
            setTaskFormData={setTaskFormData}
            showModal={showModal}
            setShowModal={setShowModal}
            createTask={createTask}
          />
        </div>

        <div className={`rightContainer ${showModal && styles.blur}`}>
          Add
          <button onClick={showData}>Ver dados</button>
        </div>
      </div>
    </>
  );
};
