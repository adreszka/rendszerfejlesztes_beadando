import React from 'react';
import { useNavigate } from "react-router-dom";

export function Teszt() {
    const navigate = useNavigate();

    const someEventHandler = () => {
        navigate('/login');
    } 

    return (
        <div>
            <button onClick={someEventHandler}>Bejelentkez�s</button>
        </div>
    );
}