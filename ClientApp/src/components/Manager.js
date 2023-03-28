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
                                <h1>�j alkatr�sz hozz�ad�sa</h1>
                                <fieldset className="input-field">
                                    <legend>Felhaszn�l�n�v:</legend>
                                    <input type="text" id="name"></input>
                                </fieldset>
                                <fieldset className="input-field">
                                    <legend>Jelsz�:</legend>
                                    <input type="number" id="price"></input>
                                </fieldset>
                                <fieldset className="input-field">
                                    <legend>Jelsz�:</legend>
                                    <input type="number" id="maxAmount"></input>
                                </fieldset>
                                <button onClick={addComponent}>Felvitel</button>
                            </div>
                        </td>
                        <td>
                            <div className="bg">
                                <h1>Alkatr�sz szerkeszt�se</h1>
                                <select className="list">
                                    {listElements()}
                                </select><br></br>
                                <fieldset className="input-field">
                                    <legend>�j �r:</legend>
                                    <input type="number" id="newPrice"></input>
                                </fieldset>
                                <button onClick={update}>Friss�t�s</button>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
}