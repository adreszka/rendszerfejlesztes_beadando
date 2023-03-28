import React from 'react';
import { useNavigate } from "react-router-dom";
import "./Manager.css";

export function Manager() {
    const addComponent = () => {
        
    }

    const update = () => {

    }

    var tomb = [1, 2];

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
                                <fieldset className="input-field">
                                    <legend>Felhasználónév:</legend>
                                    <input type="text" id="name"></input>
                                </fieldset>
                                <fieldset className="input-field">
                                    <legend>Jelszó:</legend>
                                    <input type="number" id="price"></input>
                                </fieldset>
                                <fieldset className="input-field">
                                    <legend>Jelszó:</legend>
                                    <input type="number" id="maxAmount"></input>
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
                                <fieldset className="input-field">
                                    <legend>Új ár:</legend>
                                    <input type="number" id="newPrice"></input>
                                </fieldset>
                                <button onClick={update}>Frissítés</button>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
}