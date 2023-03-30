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
                                <h1>Add new component</h1>
                                <form onSubmit={addComponent}>
                                    <fieldset className="input-field">
                                        <legend>Name:</legend>
                                        <input type="text" id="name"></input>
                                    </fieldset>
                                    <fieldset className="input-field">
                                        <legend>Price:</legend>
                                        <input type="number" id="price" min="0"></input>
                                    </fieldset>
                                    <fieldset className="input-field">
                                        <legend>Max amount:</legend>
                                        <input type="number" id="maxAmount" min="0"></input>
                                    </fieldset>
                                    <button>Add</button>
                                    </form>
                            </div>
                        </td>
                        <td>
                            <div className="bg">
                                <h1>Update component</h1>
                                <form onSubmit={update}>
                                    <select className="list">
                                        {listElements()}
                                    </select><br></br>
                                    <fieldset className="input-field">
                                        <legend>New price:</legend>
                                        <input type="number" id="newPrice" min="0"></input>
                                    </fieldset>
                                    <button>Update</button>
                                </form>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
}