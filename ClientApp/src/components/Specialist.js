import React from 'react';
import "./Specialist.css";
import { Client } from "../ApiServices.ts";

export function Specialist() {
    var client = new Client();

    var flag = false;

    //craete project
    const createProject = async (event) => {
        event.preventDefault();
        const data = new FormData(event.target);

        await client.addNewProject({
            "location": data.get('location'),
            "description": data.get('description'),
            "name": data.get('name'),
            "phoneNumber": data.get('phone'),
            "email": data.get('email'),
            "taxNumber": data.get('tax')
        }).then((val) => {
            if (val) {
                window.alert("The project was successfully created!");
            } else {
                window.alert("An error occured during the creation of the new project!");
            }
        }).catch((error) => { console.log(error) });
    };

    const setFlag = (check) => {
        flag = check.target.checked;

        document.getElementById("tax").readOnly = !flag;

        if (!flag)
            document.getElementById("tax").value = "";
    };

    //projects
    const setCurrentProject = () => {

    };

    const listProjects = () => {

    };

    //compoents
    const setCurrentComponent = () => {

    };

    const listComponents = () => {

    };

    //add component to project
    const setCurrentProjectAdd = () => {

    };

    const listProjectsAdd = () => {

    };

    const setCurrentComponentAdd = () => {

    };

    const listComponentsAdd = () => {

    };

    const addComponentToProject = async (event) => {
        event.preventDefault();
        const data = new FormData(event.data);
    };

    return (
        <div>
            <table className="panels">
                <tbody>
                    <tr>
                        <td colSpan="2">
                            <div className="bg">
                                <h1>Create project</h1>
                                <form onSubmit={createProject}>
                                    <table className="projectData">
                                        <tbody>
                                            <tr>
                                                <th className="projectHeader">Project's data</th>
                                                <th className="projectHeader">Customer's data</th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <fieldset className="input-field">
                                                        <legend>Lacation:</legend>
                                                        <input type="text" id="location" name="location"></input>
                                                    </fieldset>
                                                    <fieldset className="input-field">
                                                        <legend>Description:</legend>
                                                        <textarea id="description" name="description" className="description"></textarea>
                                                    </fieldset>
                                                </td>
                                                <td>
                                                    <fieldset className="input-field">
                                                        <legend>Name:</legend>
                                                        <input type="text" id="name" name="name"></input>
                                                    </fieldset>
                                                    <fieldset className="input-field">
                                                        <legend>Phone number:</legend>
                                                        <input type="text" id="phone" name="phone"></input>
                                                    </fieldset>
                                                    <fieldset className="input-field">
                                                        <legend>E-mail address:</legend>
                                                        <input type="text" id="email" name="email"></input>
                                                    </fieldset>
                                                    <label>
                                                        <input type="checkbox" id="company" name="company" onChange={setFlag} />
                                                        Company
                                                    </label>
                                                    <fieldset className="input-field">
                                                        <legend>Tax number:</legend>
                                                        <input type="text" id="tax" name="tax" readOnly></input>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <button>Create</button>
                                </form>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div className="bg">
                                <h1>Projects</h1>
                                <select className="list" id="listProjects" name="listProjects" onChange={setCurrentProject}>
                                    <option key="choose" value="choose" >Choose a project</option>
                                    {listProjects()}
                                </select>
                                <fieldset className="input-field">
                                    <legend>State:</legend>
                                    <input type="text" id="projectState" name="projectState" readOnly></input>
                                </fieldset>
                            </div>
                        </td>
                        <td>
                            <div className="bg">
                                <h1>Components</h1>
                                <select className="list" id="listComponents" name="listComponents" onChange={setCurrentComponent}>
                                    <option key="choose" value="choose" >Choose a component</option>
                                    {listComponents()}
                                </select>
                                <fieldset className="input-field">
                                    <legend>Price:</legend>
                                    <input type="text" id="componentPrice" name="componentPrice" readOnly></input>
                                </fieldset>
                                <fieldset className="input-field">
                                    <legend>State:</legend>
                                    <input type="text" id="componentState" name="componentState" readOnly></input>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colSpan="2">
                            <div className="bg">
                                <form onSubmit={addComponentToProject}>
                                    <h1>Add components to project</h1>
                                    <select className="list" id="listProjectsAdd" name="listProjectsAdd" onChange={setCurrentProjectAdd}>
                                        <option key="choose" value="choose" >Choose a project</option>
                                        {listProjectsAdd()}
                                    </select>
                                    <br />
                                    <textarea id="components" name="components"></textarea>
                                    <br />
                                    <select className="list" id="listComponentsAdd" name="listComponentsAdd" onChange={setCurrentComponentAdd}>
                                        <option key="choose" value="choose" >Choose a component</option>
                                        {listComponentsAdd()}
                                    </select>
                                    <fieldset className="input-field">
                                        <legend>Amount:</legend>
                                        <input type="number" id="componentAmount" name="componentAmount" min="0"></input>
                                    </fieldset>
                                    <button>Add component</button>
                                </form>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
}