import React, { useState, useEffect } from 'react';
import { useNavigate } from "react-router-dom";
import './Login.css';
import { Client } from "../ApiServices.ts";

export function Warehouseman() {
    var client = new Client();
    const [projects, setProjects] = new useState([]);
    const [currentProject, setCurrentProject] = new useState({ "statusName": "", "projectComponents": [{ "componentName": "", "location": { "row": 0, "columnn": 0, "level": 0 }, "quantity": 0 }] });

    const getProjects = async () => {
        await client.getProjects().then((val) => {
            setProjects(val);
            listProjects();
        }).catch((error) => console.log(error));
    };

    const flag = null;

    useEffect(() => {
        getProjects();
    }, [flag])

    //list projects
    const listProjects = () => {
        var result = new Array();

        result = result.concat(projects.map((temp) => {
            var name = temp['location'];

            return <option key={name} value={name} >{name}</option>;
        }));

        return result;
    };

    //set current project
    const setCurrent = async (option) => {
        await client.listProject(option.target.value).then((val) => {
            setCurrentProject(val);
            document.getElementById("projectState").value = val["statusName"];
            listComponents(val);
        }).catch((error) => { console.log(error) });
    }

    //list components
    const listComponents = (current) => {
        var select = document.getElementById("listProjectsC");

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

        if (document.getElementById("listProjects") != "choose") {
            var list = current["projectComponents"];

            for (var i = 0; i < list.length; i++) {
                var temp = (i + 1) + ".: " + list[i]["componentName"] + " (" + list[i]["quantity"] + ")";
                var option = document.createElement('option');
                option.key = temp;
                option.value = list[i]["componentName"];
                option.innerHTML = temp;

                select.appendChild(option);
            }
        }
    }

    //list positions
    const listPositions = (option) => {
        var positions = currentProject["projectComponents"];

        for (var i = 0; i < positions.length; i++)
        {
            if (positions[i]["componentName"] === option.target.value) {
                var coordinates = positions[i]["location"];
                document.getElementById("position").value = "row: " + coordinates["row"] + ", column: " + coordinates["columnn"] + ", level: " + coordinates["level"];
                break;
            }
        }
    }

    //take components
    const takeComponents = async (event) => {
        event.preventDefault();
        const data = new FormData(event.target);

        
    }

    return (
        <div>
            <div className="bg">
                <h1>Projects</h1>
                <form onSubmit={takeComponents}>
                    <select className="list" id="listProjects" name="listProjects" onChange={setCurrent}>
                        <option key="choose" value="choose" >Choose a project</option>
                        {listProjects()}
                    </select><br />
                    <fieldset className="input-field">
                        <legend>State:</legend>
                        <input type="text" id="projectState" readOnly></input>
                    </fieldset>
                    <select className="list" id="listProjectsC" onChange={listPositions}>
                        <option key="choose" value="choose" >Choose a component</option>
                    </select><br/>
                    <input type="text" id="position" readOnly></input><br /><br />
                    <button>Take components</button>
                </form>
            </div>

        </div>
    );
}