import { createBrowserRouter } from "react-router-dom";
import { TaskPage } from "./routes/task-page/task-page";
import { AboutPage } from "./routes/about-page/about-page";
import { App } from "./App";

export const router = createBrowserRouter([
    { path: "/", element: <App /> },
    { path: "/task", element: <TaskPage /> },
    { path: "/about", element: <AboutPage /> }
  ]);