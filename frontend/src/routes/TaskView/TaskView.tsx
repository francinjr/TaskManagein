import { useEffect, useState } from "react";
import { TaskInterface } from "../../interfaces/TaskInterface";
import { NavBar } from "../../components/Navbar/NavBar";
import styles from "./styles.module.css";
import { Task } from "../../components/Task/Task";
import { TaskForm } from "../../components/Forms/TaskForm";
import TaskService from "../../services/TaskService";
import { AxiosResponse } from "axios";

const defaultTaskFormData: TaskInterface = {
  name: "",
  description: "",
  status: 1,
  id: 0,
};
export const TaskView = () => {
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

  function createTask(): void {
    // chamar a api

    const newTask: TaskInterface = {
      ...taskFormData,
      id: tasks.length + 1,
    };

    setTasks([...tasks, newTask]);

    setShowModal(false);
    setTaskFormData({ ...defaultTaskFormData });
  }

  function openModal() {
    setShowModal(true);
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
                    />
                  ))}

                {!tasks.length && <p>Não há nenhuma tarefa</p>}
              </div>
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
