import { useState } from "react";
import { TaskInterface } from "../../interfaces/TaskInterface";
import { NavBar } from "../../components/Navbar/NavBar";
import styles from "./styles.module.css";
import { Task } from "../../components/Task";
import { TaskForm } from "../../components/Forms/TaskForm";

const data: TaskInterface[] = [
    {
      name: "Estudar para a prova de Estruturas de Dados 1",
      description: "Focar nas implementações das estruturas de dados, principamente listas encadeadas",
      status: 1,
      id: 1
    },
    {
      name: "Se aprofundar ainda mais em Java, C# e React",
      description: "Programação é incrível",
      status: 2,
      id: 2
    },
    {
      name: "Estudar Design Patterns para a prova de Laboratório de Programação Orientada a Objetos",
      description: "Adapter é incrível",
      status: 3,
      id: 3
    },
  ];
  
  const defaultTaskFormData: TaskInterface = {
    name: "",
    description: "",
    status: 1,
    id: 0
  };
export const TaskView = () => {
    const [tasks, setTasks] = useState(data);
    const [taskFormData, setTaskFormData] = useState<TaskInterface>({ ...defaultTaskFormData });
    const [showModal, setShowModal] = useState(false);
  
    function handleCompleteTask(id: number) {
      const taskIndex = tasks.findIndex(item => item.id === id);
  
      if(taskIndex === -1) {
        return;
      }
  
      const newTasks = [ ...tasks ];
  
      if(newTasks[taskIndex].status == 1) {
        newTasks[taskIndex].status = 2;
      } else if(newTasks[taskIndex].status == 2) {
        newTasks[taskIndex].status = 3;
      }
  
      setTasks(newTasks);
    }
  
    function createTask(): void {
      // chamar a api
  
  
      const newTask: TaskInterface = { 
        ...taskFormData,
        id: tasks.length + 1
      }
  
      setTasks([ ...tasks, newTask ])
  
      setShowModal(false)
      setTaskFormData({ ...defaultTaskFormData });
    }
  
  
    function openModal() {
      setShowModal(true);
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
                <input className="input" placeholder="Pesquisar tarefa" type="text" />
                <button className="button" onClick={openModal}>Nova</button>
              </div>
        
              <div className="task">
                {tasks.length > 0 && tasks.map(item => (
                  <Task key={item.id} tasks={item} handleCompleteTask={handleCompleteTask} />
                ))}
        
                {!tasks.length && <p>Não há nenhuma tarefa</p>}
              </div>
            </main>
          </div>
          <TaskForm taskFormData={taskFormData} setTaskFormData={setTaskFormData} showModal={showModal} setShowModal={setShowModal} createTask={createTask} />
        </div>

        <div className={`rightContainer ${showModal && styles.blur}`}>
            Add
        </div>
      </div>
      </>
    );
}