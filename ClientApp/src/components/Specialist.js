import React, { useState, useEffect } from 'react';
import "./Specialist.css";
import { Client } from "../ApiServices.ts";

export function Specialist() {
    var client = new Client();
    const [projects, setProjects] = new useState([]);
    const [components, setComponents] = new useState([]);

    const getProjects = async () => {
        await client.getProjects().then((val) => {
            setProjects(val);
            listProjects();
            listProjectsAdd();
            listProjectsProperties();
        }).catch((error) => console.log(error));
    };

    const getComponents = async () => {
        await client.getAll().then((val) => {
            setComponents(val);
            listComponents();
            listComponentsAdd();
        }).catch((error) => console.log(error));
    };

    const flag = null;

    useEffect(() => {
        getProjects();
        getComponents();
    }, [flag])

    //create project
    var companyFlag = false;

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
                window.alert("An error occurred during the creation of the new project!");
            }
        }).catch((error) => { console.log(error) });
    };

    const setFlag = (check) => {
        companyFlag = check.target.checked;

        document.getElementById("tax").readOnly = !companyFlag;

        if (!companyFlag)
            document.getElementById("tax").value = "";
    };

    //projects
    const setCurrentProject = (option) => {
        if (option.target.value !== "choose") {
            client.getProjectsWithStatus(option.target.value).then((val) => {
                document.getElementById("projectState").value = val['statusName'];
            }).catch((error) => { console.log(error) });
        } else {
            document.getElementById("projectState").value = "";
        }   
    };

    const listProjects = () => {
        var result = new Array();

        result = result.concat(projects.map((temp) => {
            var name = temp['location'];

            return <option key={name} value={name} >{name}</option>;
        }));

        return result;
    };

    //compoents
    const setCurrentComponent = (option) => {
        if (option.target.value !== "choose") {
            client.getAvailableComponent(option.target.value).then((val) => {
                document.getElementById("componentPrice").value = val['price'];

                if (val['availableQuantity'] > 0) {
                    document.getElementById("componentState").value = "available - " + val['availableQuantity'];
                } else {
                    document.getElementById("componentState").value = "unavailable";
                }
                
            }).catch((error) => { console.log(error) });
        } else {
            document.getElementById("componentPrice").value = "";
            document.getElementById("componentState").value = "";
        }
    };

    const listComponents = () => {
        var result = new Array();

        result = result.concat(components.map((temp) => {
            var name = temp['name'];

            return <option key={name} value={name} >{name}</option>;
        }));

        return result;
    };

    //add component to project
    const setCurrentProjectAdd = (option) => {
        if (option.target.value !== "choose") {
            client.getProjectComponents(option.target.value).then((val) => {
                var text = "";
                val.forEach((temp) => {
                    text += temp['name'] + " - " + temp['quantity'] + "\n";
                });

                document.getElementById("components").value = text;
            }).catch((error) => { console.log(error); });
        } else { 
            document.getElementById("components").value = "";
        }
    };

    const listProjectsAdd = () => {
        var result = new Array();

        result = result.concat(projects.map((temp) => {
            var name = temp['location'];

            return <option key={name} value={name} >{name}</option>;
        }));

        return result;
    };

    const setCurrentComponentAdd = (option) => {

    };

    const listComponentsAdd = () => {
        var result = new Array();

        result = result.concat(components.map((temp) => {
            var name = temp['name'];

            return <option key={name} value={name} >{name}</option>;
        }));

        return result;
    };

    const addComponentToProject = async (event) => {
        event.preventDefault();
        const data = new FormData(event.target);

        await client.addComponentToProject({
            "location": data.get("selectProjectsAdd"),
            "name": data.get("selectComponentsAdd"),
            "quantity": data.get("componentAmount")
        }).then((val) => {
            if (val) {
                window.alert("The component addition to the project was successful!");
            } else {
                window.alert("There was an issue during the addition of the component to the project!");
            }
        }).catch((error) => { console.log(error) });

        if (data.get("selectProjectsAdd") !== "choose") {
            client.getProjectComponents(data.get("selectProjectsAdd")).then((val) => {
                var text = "";
                val.forEach((temp) => {
                    text += temp['name'] + " - " + temp['quantity'] + "\n";
                });

                document.getElementById("components").value = text;
            }).catch((error) => { console.log(error); });
        } else {
            document.getElementById("components").value = "";
        }
    };

    //set project properties
    const setCurrentProjectProperties = (option) => {

    };

    const listProjectsProperties = () => {
        var result = new Array();

        result = result.concat(projects.map((temp) => {
            var name = temp['location'];

            return <option key={name} value={name} >{name}</option>;
        }));

        return result;
    };

    const setProjectProperties = async (event) => {
        event.preventDefault();
        const data = new FormData(event.target);

        await client.addWorkTimeAndFee({
            "worktime": data.get('projectDays'),
            "fee": data.get('projectFee'),
            "location": data.get('selectProjectsProperties')
        }).then((val) => {
            if (val) {
                window.alert("Project properties were successfully set!");
            } else {
                window.alert("There was an issue during the set of the properties!");
            }
        }).catch((error) => { console.log(error) });
    };

    //calculate price
    const calculatePrice = async (event) => {
        event.preventDefault();
        const data = new FormData(event.target);

        await client.priceCalculation(document.getElementById("selectProjects").value).then((val) => {
            if (val) {
                window.alert("The calculation was successful!");
            } else {
                window.alert("There was an issue durind the calculation!");
            }
        }).catch((error) => { console.log(error) });
    };

    //finish project
    const finishProject = async (event) => {
        event.preventDefault();
        const data = new FormData(event.target);

        await client.closeProject({
            "location": document.getElementById("selectProjects").value,
            "projectFinished": document.getElementById("completed").checked
        }).then((val) => {
            if (val) {
                window.alert("The project was successfully finished!");
            } else {
                window.alert("the project cannot be finished!");
            }
        }).catch((error) => { console.log(error) });
    }

    //append component to project
    const setAppendComponent = async (option) => {
        var text = document.getElementById("selectProjectsAppend").value;
        var select = document.getElementById("selectComponentsAppend");

        {
            for (var i = select.options.length - 1; i >= 0; i--) {
                select.options[0] = null;
            }
            var option = document.createElement('option');
            option.key = "choose";
            option.value = "choose";
            option.innerHTML = "Choose a component";

            select.appendChild(option);
        }

        if (text !== "choose") {
            var list = new Array();

            await client.getProjectComponents(text).then((val) => {
                list = val;
            }).catch((error) => { console.log(error) });

            for (var i = 0; i < list.length; i++) {
                var option = document.createElement('option');
                option.key = list[i]["name"];
                option.value = list[i]["quantity"];
                option.innerHTML = list[i]["name"];

                select.appendChild(option);
            }
        }
    }

    const setAppendPosition = async (option) => {
        var text = document.getElementById("selectComponentsAppend").value;
        var select = document.getElementById("selectPositionsAppend");

        {
            for (var i = select.options.length - 1; i >= 0; i--) {
                select.options[0] = null;
            }
            var option = document.createElement('option');
            option.key = "choose";
            option.value = "choose";
            option.innerHTML = "Choose a position";

            select.appendChild(option);
        }

        if (text !== "choose") {
            var list = new Array();

            await client.storageAll(option.target.value).then((val) => {
                list = val;
            }).catch((error) => { console.log(error) });

            for (var i = 0; i < list.length; i++) {
                var option = document.createElement('option');
                option.key = list[i]["row"] + " " + list[i]["column"] + " " + list[i]["level"];
                option.value = list[i]["freeComponent"]["name"] + " " + list[i]["freeComponent"]["quantity"];
                option.innerHTML = list[i]["name"];

                select.appendChild(option);
            }
        }
    }

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
                                <table className="buttons">
                                    <tbody>
                                        <tr>
                                            <td colSpan="2">
                                                <select className="list" id="selectProjects" name="selectProjects" onChange={setCurrentProject}>
                                                    <option key="choose" value="choose" >Choose a project</option>
                                                    {listProjects()}
                                                </select>
                                                <fieldset className="input-field">
                                                    <legend>State:</legend>
                                                    <input type="text" id="projectState" name="projectState" readOnly></input>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <form onSubmit={calculatePrice}>
                                                    <button>Calculate price</button>
                                                </form>
                                            </td>
                                            <td>
                                                <form onSubmit={finishProject}>
                                                    <input type="radio" id="completed" name="finishRadio" value="completed"></input>
                                                    <label for="completed">completed</label>
                                                    <br/>
                                                    <input type="radio" id="failed" name="finishRadio" value="failed"></input>
                                                    <label for="failed">failed</label>
                                                    <br/>
                                                    <button>Finish project</button>
                                                </form>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </td>
                        <td>
                            <div className="bg">
                                <h1>Components</h1>
                                <select className="list" id="selectComponents" name="selectComponents" onChange={setCurrentComponent}>
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
                        <td>
                            <div className="bg">
                                <form onSubmit={addComponentToProject}>
                                    <h1>Add components to project</h1>
                                    <select className="list" id="selectProjectsAdd" name="selectProjectsAdd" onChange={setCurrentProjectAdd}>
                                        <option key="choose" value="choose" >Choose a project</option>
                                        {listProjectsAdd()}
                                    </select>
                                    <br />
                                    <textarea id="components" name="components" readOnly></textarea>
                                    <br />
                                    <select className="list" id="selectComponentsAdd" name="selectComponentsAdd" onChange={setCurrentComponentAdd}>
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
                        <td>
                            <div className="bg">
                                <form onSubmit={setProjectProperties}>
                                    <h1>Set project properties:</h1>
                                    <select className="list" id="selectProjectsProperties" name="selectProjectsProperties" onChange={setCurrentProjectProperties}>
                                        <option key="choose" value="choose" >Choose a project</option>
                                        {listProjectsProperties()}
                                    </select>
                                    <fieldset className="input-field">
                                        <legend>Work days:</legend>
                                        <input type="number" id="projectDays" name="projectDays" min="0"></input>
                                    </fieldset>
                                    <fieldset className="input-field">
                                        <legend>Work fee:</legend>
                                        <input type="number" id="projectFee" name="projectFee" min="0"></input>
                                    </fieldset>
                                    <button>Set properties</button>
                                </form>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colSpan="2">
                            <div className="bg">
                                <h1>Append components to project</h1>
                                <form>
                                    <fieldset className="input-field">
                                        <legend>Projects:</legend>
                                        <select className="list" id="selectProjectsAppend" name="selectProjectsAppend" onChange={setAppendComponent}>
                                            <option key="choose" value="choose" >Choose a project</option>
                                            {listProjects()}
                                        </select>
                                    </fieldset>
                                    <fieldset className="input-field">
                                        <legend>Components:</legend>
                                        <select className="list" id="selectComponentsAppend" name="selectComponentsAppend" onChange={setAppendPosition}>
                                            <option key="choose" value="choose" >Choose a component</option>
                                        </select>
                                    </fieldset>
                                    <fieldset className="input-field">
                                        <legend>Positions:</legend>
                                        <select className="list" id="selectPositionsAppend" name="selectPositionsAppend">
                                            <option key="choose" value="choose" >Choose a position</option>
                                        </select>
                                    </fieldset>
                                    <button>Append</button>
                                </form>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
}