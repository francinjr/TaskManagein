import { NavBar } from "./components/nav-bar/nav-bar";

export function App() {
  return (
    <>
    <div className="mainContainer">
      <div className="navContainer">
        <NavBar />
      </div>

      <div className="centerContainer">
        Página inicial
      </div>

      <div className="rightContainer">
        Add
      </div>
    </div>
    
      
    </>
  )
}
