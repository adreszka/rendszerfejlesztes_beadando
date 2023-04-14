import React, { useState, useEffect } from 'react';
import "./Manager.css";
import { Client } from "../ApiServices.ts";

export function Manager() {
    var client = new Client();
    const [elements, setElements] = useState([]);
    const flag = null;

    useEffect(() => {
        getElements();
    },[flag])

    const addComponent = async (event) => {
        event.preventDefault();
        const data = new FormData(event.target);

        await client.componentPOST({
            "name": data.get('name'),
            "price": data.get('price'),
            "maxCapacity": data.get('maxAmount')
        }).then((val) => {
            if (val === true) {
                window.alert("Component successfully added");
            } else {
                window.alert("The component is already in the database!");
            }
        }).catch((error) => { console.log(error) });

        getElements();
        listElements();
    }

    const getElements = async () => {
        await client.componentAll().then((val) => {
            setElements(val);
            console.log(val);
            listElements();
        }).catch((error) => console.log(error));
    }

    const listElements = () => {
        var result = new Array(<option key="choose" value="choose" >Choose a component</option>);

        result = result.concat(elements.map((temp) => {
            var name = temp['name'];

            return <option key={name} value={name} >{name}</option>;
        }));

        return result;
    }

    const updateComponent = (event) => {
        event.preventDefault();
        const data = new FormData(event.target);

        if (document.getElementById('list').value != "choose") {
            client.componentPUT({
                "name": data.get('list'),
                "price": data.get('newPrice'),
                "maxCapacity": 0
            }).then((val) => {
                if (val) {
                    window.alert("The price of the component was successfully changed!");
                } else {
                    window.alert("An error occurred during the change of the price!");
                }
            }).catch((error) => { console.log(error) });
        }

        getElements();
        listElements();
    };

    const setCurrentComponent = (option) => {
        getElements();

        if (option.target.value != "choose") {
            document.getElementById('currentPrice').value = elements.find((temp) => { return temp['name'] == option.target.value })['price'];
        } else {
            document.getElementById('currentPrice').value = "";
        }
    };

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
                                        <input type="text" id="name" name="name"></input>
                                    </fieldset>
                                    <fieldset className="input-field">
                                        <legend>Price:</legend>
                                        <input type="number" id="price" min="0" name="price"></input>
                                    </fieldset>
                                    <fieldset className="input-field">
                                        <legend>Max amount:</legend>
                                        <input type="number" id="maxAmount" min="0" name="maxAmount"></input>
                                    </fieldset>
                                    <button>Add</button>
                                    </form>
                            </div>
                        </td>
                        <td>
                            <div className="bg">
                                <h1>Update component</h1>
                                <form onSubmit={updateComponent}>
                                    <select className="list" id="list" name="list" onChange={setCurrentComponent}>
                                        {listElements()}
                                    </select><br></br>
                                    <fieldset className="input-field">
                                        <legend>Current price:</legend>
                                        <input type="text" id="currentPrice" name="currentPrice" readOnly></input>
                                    </fieldset>
                                    <fieldset className="input-field">
                                        <legend>New price:</legend>
                                        <input type="number" id="newPrice" name="newPrice" min="0"></input>
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