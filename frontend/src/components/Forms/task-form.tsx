import React from 'react';
import styles from './styles.module.css';
import { TaskInterface } from '../../interfaces/task-interface';

interface TaskFormProps {
  taskFormData: TaskInterface;
  setTaskFormData: React.Dispatch<React.SetStateAction<TaskInterface>>;
  showModal: boolean;
  setShowModal: React.Dispatch<React.SetStateAction<boolean>>;
  createTask: () => void;
}

export function TaskForm(props: TaskFormProps) {
    const options = ["A fazer", "Em andamento", "Concluída"];
  
    const closeModal = () => {
      props.setShowModal(false);
    };

    const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = event.target;
        props.setTaskFormData(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleSelectChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        props.setTaskFormData(prevState => ({
            ...prevState,
            status: convertStringToIntegerStatus(event.target.value)
        }));
    };

    function convertStringToIntegerStatus(status: string): number  {
      switch(status) {
          case "A fazer":
          return 1;

          case "Em andamento":
              return 2;
          
          case "Concluída":
              return 3;

          default:
              return 0;
      }
  }
  
    return (
      <>
        {props.showModal && (
            <div className={styles.formContainer}>
              <span className={styles.formTitle}>Nova tarefa</span>
              <span className={styles.formClose} onClick={closeModal}>&times;</span>
              <div className={styles.inputContainer}>
              <input className="input" name="name" value={props.taskFormData.name} onChange={handleInputChange} placeholder="Nome da tarefa" type="text" />
                  <input className="input" name="description" value={props.taskFormData.description} onChange={handleInputChange} placeholder="Descrição" type="text" />
                  <select className="input" onChange={handleSelectChange}>
                      {options.map((option, index) => (
                      <option key={index} value={option}>{option}</option>
                      ))}
                  </select>
                  {/*<br /><p>Valor selecionado: {props.taskFormData.status}</p>*/}
              </div>

                  <div className={styles.buttonContainer}>
                    <button className="cancelButton" onClick={closeModal}>Cancelar</button>
                    <button className="button" onClick={props.createTask}>Salvar</button>
                  </div>
            </div>
        )}
      </>
    );
}













Parte inicial da Tabs Generica:
"use client";

import { useState } from "react";

interface TabsProps {
  tabs: string[];
  color: string;
}

export function Tabs(props: TabsProps) {
  const [activeTab, setActiveTab] = useState(props.tabs.length > 0 ? props.tabs[0] : "");

  if (props.tabs.length === 0) {
    return <div className="text-red-500">Nenhuma aba disponível</div>;
  }

  return (
    <nav className="w-full h-[4vh] flex items-center justify-between px-8">
      <div className="flex space-x-6 mr-auto text-lg">
        {props.tabs.map((tab) => (
          <button
            key={tab}
            className={`cursor-pointer ${activeTab === tab
                ? `text-${props.color} font-bold`
                : `hover:text-${props.color}`
              }`}
            onClick={() => setActiveTab(tab)}
          >
            {tab}
          </button>
        ))}
      </div>
    </nav>
  );
};
