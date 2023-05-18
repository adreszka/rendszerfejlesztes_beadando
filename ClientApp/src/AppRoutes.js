import { Login } from "./components/Login";
import { Admin } from "./components/Admin";
import { Manager } from "./components/Manager";
import { Specialist } from "./components/Specialist";
import { Warehouseman } from "./components/Warehouseman";

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
    },
    {
        path: "/warehouseman",
        element: <Warehouseman />
    },
];

export default AppRoutes;
