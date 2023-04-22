import { Login } from "./components/Login";
import { Admin } from "./components/Admin";
import { Manager } from "./components/Manager";
import { Specialist } from "./components/Specialist";

const AppRoutes = [
    {
        index: true,
        element: <Login />
    },
    {
        path: '/login',
        element: <Login />
    },
    {
        path: "/admin",
        element: <Admin />
    },
    {
        path: "/manager",
        element: <Manager />
    },
    {
        path: "/specialist",
        element: <Specialist />
    }
];

export default AppRoutes;
