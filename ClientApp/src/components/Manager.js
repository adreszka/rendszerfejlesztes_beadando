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
                                <h1>�j alkatr�sz hozz�ad�sa</h1>
                                <fieldset className="input-field" id="name">
                                    <legend>Felhaszn�l�n�v:</legend>
                                    <input type="text"></input>
                                </fieldset>
                                <fieldset className="input-field" id="price">
                                    <legend>Jelsz�:</legend>
                                    <input type="number"></input>
                                </fieldset>
                                <fieldset className="input-field" id="maxAmount">
                                    <legend>Jelsz�:</legend>
                                    <input type="number"></input>
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
                                <button onClick={update}>Friss�t�s</button>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
}