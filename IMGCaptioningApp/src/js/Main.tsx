import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import { Capacitor } from "@capacitor/core";
import { CapacitorSQLite, SQLiteConnection } from "@capacitor-community/sqlite";
import { JeepSqlite } from "jeep-sqlite/dist/components/jeep-sqlite";
import { DarkModeProvider } from './components/DarkModeContext';

window.addEventListener("DOMContentLoaded", async () => {
    try {
        const platform = Capacitor.getPlatform();
        if (platform === "web") {
            const sqlite = new SQLiteConnection(CapacitorSQLite);
            customElements.define("jeep-sqlite", JeepSqlite);
            const jeepSqliteEl = document.createElement("jeep-sqlite");
            document.body.appendChild(jeepSqliteEl);
            await customElements.whenDefined("jeep-sqlite");
            await sqlite.initWebStore();
        }
        const root = ReactDOM.createRoot(document.getElementById('root'));
        root.render(
            <DarkModeProvider>
                <App/>
            </DarkModeProvider>  
        );
    } catch (e) {
        console.log(e);
    }
});