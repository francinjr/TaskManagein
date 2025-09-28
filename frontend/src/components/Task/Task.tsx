import { TaskInterface } from "../../interfaces/task-interface";
import styles from "./styles.module.css";

interface TaskProps {
  tasks: TaskInterface;
  handleCompleteTask: (id: number) => void;
  deleteTask: (id: number) => void;
}

export function Task(props: TaskProps) {
  function showTaskStatus(status: number | string | string[]): string {
    switch (status) {
      case 1:
        return "A fazer";

      case 2:
        return "Em andamento";

      case 3:
        return "Concluída";

      default:
        return "Sem status";
    }
  }

  function defineTaskStyle(status: number | string | string[]): string {
    switch (status) {
      case 1:
        return "toDo";
      case 2:
        return "inProgress";
      case 3:
        return "concluded";
      default:
        return "";
    }
  }
  return (
    <div className={styles.taskContainer}>
      <div className="taskContentSt">
        <span className={styles.taskTitle}>{props.tasks.name}</span>
        <hr />
        <span className={styles.taskContent}>{props.tasks.description}</span>
        <hr />
        <span className={styles.taskContent}>
          {showTaskStatus(props.tasks.status)}
        </span>
        <hr />
      </div>
      <div className="taskActionContainer">
        <div className="marginActionItem">
          <img src="/searchIcon.svg" alt="Icone editar" width="40" height="40"/>
        </div>
        <div className="marginActionItem">
          <img src="/editIcon.svg" alt="Icone editar" width="40" height="40"/>
        </div>
        
        <div className="marginActionItem">
          <img src="/removeIcon.svg" alt="Icone editar" width="40" height="40" onClick={() => props.deleteTask(props.tasks.id)}/>
        </div>
      </div>
    </div>
  );
}
