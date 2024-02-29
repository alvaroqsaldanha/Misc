import { useEffect, useRef, useState } from "react";
import { SQLiteConnection, CapacitorSQLite, } from "@capacitor-community/sqlite";
import katakanaData from "./jsonData/katakanaData";
import hiraganaData from "./jsonData/hiraganaData";
import { n4Kanji, n5Kanji } from "./jsonData/kanjiData";

const useSQLiteDB = () => {
    const db = useRef();
    const sqlite = useRef();
    const [initialized, setInitialized] = useState(false);

    useEffect(() => {
        const initializeDB = async () => {
            if (sqlite.current) return;

            sqlite.current = new SQLiteConnection(CapacitorSQLite);
            const ret = await sqlite.current.checkConnectionsConsistency();
            const isConn = (await sqlite.current.isConnection("db_vite", false))
                .result;

            if (ret.result && isConn) {
                db.current = await sqlite.current.retrieveConnection("db_vite", false);
            } else {
                db.current = await sqlite.current.createConnection(
                    "db_vite",
                    false,
                    "no-encryption",
                    1,
                    false
                );
            }
        };

        initializeDB().then(() => {
            initializeTables().then(() => setInitialized(true));
        });
    }, []);

    const initializeTables = async () => {
        await performSQLAction(async (db: SQLiteConnection | null) => { 
            const queryCreateTable = `
            CREATE TABLE IF NOT EXISTS pastCaptions (
                caption TEXT NOT NULL,
                image TEXT NOT NULL,
                format TEXT NOT NULL
            );`;
            await db?.execute(queryCreateTable);
        });
    };

    const performSQLAction = async (
        action,
        cleanup
    ) => {
        try {
            await db.current?.open();
            await action(db.current);
        } catch (error) {
            console.log((error));
        } finally {
            try {

                (await db.current?.isDBOpen())?.result && (await db.current?.close());
                cleanup && (await cleanup());
            } catch { }
        }
    };





    return { performSQLAction, initialized};
};

export default useSQLiteDB;