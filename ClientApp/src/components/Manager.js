import React from 'react';
import { useNavigate } from "react-router-dom";
import "./Manager.css";

export function Manager() {
    const addComponent = () => {
        
    }

    const update = () => {

    }

    const listElements = () => {
        var result = tomb.map((temp) => {
            return <option value={temp}>{temp}</option>;
        });

        return result;
    }

    return (
        <div>
            <table className="panels">
                <tbody>
                    <tr>
                        <td>
                            <div className="bg">
                                <h1>Új alkatrész hozzáadása</h1>
                                <fieldset className="input-field" id="name">
                                    <legend>Felhasználónév:</legend>
                                    <input type="text"></input>
                                </fieldset>
                                <fieldset className="input-field" id="price">
                                    <legend>Jelszó:</legend>
                                    <input type="number"></input>
                                </fieldset>
                                <fieldset className="input-field" id="maxAmount">
                                    <legend>Jelszó:</legend>
                                    <input type="number"></input>
                                </fieldset>
                                <button onClick={addComponent}>Felvitel</button>
                            </div>
                        </td>
                        <td>
                            <div className="bg">
                                <h1>Alkatrész szerkesztése</h1>
                                <select className="list">
                                    {listElements()}
                                </select><br></br>
                                <button onClick={update}>Frissítés</button>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
}