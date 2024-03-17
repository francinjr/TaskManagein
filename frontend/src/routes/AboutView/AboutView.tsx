import { NavBar } from "../../components/Navbar/NavBar"

export const AboutView = () => {
    return (
        <>
        <div className="mainContainer">
            <div className="navContainer">
                <NavBar />
            </div>

            <div className="centerContainer">
                Gerencie suas tarefas nessa aplicação
            </div>

            <div className="rightContainer">
                Add
            </div>
        </div>

        </>
    )
}