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
    }

    const getElements = async () => {
        await client.componentAll().then((val) => {
            setElements(val);
            listElements();
        }).catch((error) => console.log(error));
    }

    const listElements = () => {
        var result = new Array();

        result = result.concat(elements.map((temp) => {
            var name = temp['name'];

            return <option key={name} value={name} >{name}</option>;
        }));

        return result;
    }

    const updateComponent = (event) => {
        event.preventDefault();
        const data = new FormData(event.target);

        if (document.getElementById('listUpdate').value !== "choose") {
            client.componentPUT({
                "name": data.get('listUpdate'),
                "price": data.get('newPrice'),
                "maxCapacity": 0
            }).then((val) => {
                if (val) {
                    window.alert("The price of the component was successfully changed!");
                } else {
                    window.alert("An error occured during the change of the price!");
                }
            }).catch((error) => { console.log(error) });
        }

        getElements();
    };

    const setCurrentComponentOnUpdate = (option) => {
        getElements();

        if (option.target.value !== "choose") {
            document.getElementById('currentPrice').value = elements.find((temp) => { return temp['name'] === option.target.value })['price'];
        } else {
            document.getElementById('currentPrice').value = "";
        }
    };

    //store component
    const storeComponent = async (event) => {
        event.preventDefault();
        const data = new FormData(event.target);

        if (data.get('listStore') !== "choose") {
            client.storage({
                "name": data.get('listStore'),
                "quantity": data.get('amountToStore')
            }).then((val) => {
                if (val === 0) {
                    window.alert("All the componenets have been stored in the warehouse!");
                } else {
                    window.alert("There hasn't been enough space for all the components!\nRemained components: " + val);
                }
            }).catch((error) => { console.log(error) });
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
                                    <select className="list" id="listUpdate" name="listUpdate" onChange={setCurrentComponentOnUpdate}>
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
                        <td>
                            <div className="bg">
                                <h1>Store component</h1>
                                <form onSubmit={storeComponent}>
                                    <select className="list" id="listStore" name="listStore">
                                        {listElements()}
                                    </select><br></br>
                                    <fieldset className="input-field">
                                        <legend>Amount:</legend>
                                        <input type="number" id="amountToStore" name="amountToStore" min="0"></input>
                                    </fieldset>
                                    <button>Store</button>
                                </form>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
}