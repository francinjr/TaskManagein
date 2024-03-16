import { TaskInterface } from '../../interfaces/TaskInterface'
import styles from './styles.module.css'

interface TaskProps {
    tasks: TaskInterface;
    handleCompleteTask: (id: number) => void;
}

export function Task(props: TaskProps) {
    function showTaskStatus(status: number | string | string[]): string {
        switch(status) {
            case 1:
                return "A fazer"

            case 2:
                return "Em andamento"
            
            case 3:
                return "Concluída"

            default:
                return "Sem status"
        }
    }

    function defineTaskStyle(status: number | string | string[]) :string {
        switch(status) {
            case 1:
                return "toDo"
            case 2:
                return "inProgress"
            case 3:
                return "concluded"
            default:
                return ""
        }
    }
    return (
        <div className={styles.taskContainer}>
            {/*<p className={`${styles.taskTitle} ${props.tasks.status != 1 && styles.taskCompleted}`}>*/}
            <p className={`${defineTaskStyle(props.tasks.status)}`}>
                {props.tasks.id} - {props.tasks.name} - {props.tasks.description} - {showTaskStatus(props.tasks.status)}
            </p>
            {props.tasks.status != 3 && (
                <button onClick={() => props.handleCompleteTask(props.tasks.id)} className={styles.taskButton}>Ok</button>
            )}
        </div>
    )
}