import { Link } from "react-router-dom"
import styles from "./styles.module.css"

export const NavBar = () => {
    return (
        <>
        <div className={styles.navBar}>
            <Link to="/"><button className={styles.navBarButton}>Home</button></Link>
            <Link to="/task"><button className={styles.navBarButton}>Tarefas</button></Link>
            <Link to="/about"><button className={styles.navBarButton}>Sobre</button></Link>
        </div>

        </>
    )
}