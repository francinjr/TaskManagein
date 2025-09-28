import { NavBar } from "../../components/nav-bar/nav-bar"


export const AboutPage = () => {
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