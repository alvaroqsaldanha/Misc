import React from 'react';
import { useDarkMode } from './DarkModeContext';
import { FormGroup, FormControlLabel, Grid, Switch, FormControl, InputLabel, NativeSelect } from '@mui/material';
import { SelectChangeEvent } from '@mui/material/Select';

const Settings: React.FC = ({setModel,model}) => {

    const { darkMode, toggleDarkMode } = useDarkMode();  

    const handleChangeModel = (event: SelectChangeEvent) => {
        setModel(event.target.value);
    };

    return (
        <Grid
            container
            spacing={2}
            direction="column"
            justifyContent="center"
            alignItems="center"
            style={{ minHeight: '100vh' }}
        >
            <FormGroup>
                <FormControlLabel control={<Switch color="secondary" checked={darkMode} onChange={toggleDarkMode} />} label="Dark Mode" />
                <br></br>
                <FormControl fullWidth>
                    <InputLabel variant="standard" htmlFor="uncontrolled-native" className={darkMode ? 'dark' : 'light'}>
                        Model
                    </InputLabel>
                    <NativeSelect className={darkMode ? 'dark' : 'light'}
                        defaultValue={model}
                        inputProps={{
                            name: 'model',
                            id: 'uncontrolled-native',
                        }}
                        onChange={handleChangeModel}
                    >
                        <option value={"https://api-inference.huggingface.co/models/Salesforce/blip-image-captioning-large"} className={darkMode ? 'dark' : 'light'} >'Salesforce BLIP - Large'</option>
                        <option value={"https://api-inference.huggingface.co/models/nlpconnect/vit-gpt2-image-captioning"} className={darkMode ? 'dark' : 'light'} >'NLPCONNECT Vit-gpt2-image-captioning'</option>
                        <option value={"https://api-inference.huggingface.co/models/microsoft/git-base"} className={darkMode ? 'dark' : 'light'} >'GIT (GenerativeImage2Text), base-sized'</option>
                    </NativeSelect>
                </FormControl>
            </FormGroup>
        </Grid>
    );
};

export default Settings;