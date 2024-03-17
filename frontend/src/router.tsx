import { createBrowserRouter } from "react-router-dom";
import { TaskView } from "./routes/TaskView/TaskView";
import { AboutView } from "./routes/AboutView/AboutView";
import { App } from "./App";

export const router = createBrowserRouter([
    { path: "/", element: <App /> },
    { path: "/task", element: <TaskView /> },
    { path: "/about", element: <AboutView /> }
  ]);