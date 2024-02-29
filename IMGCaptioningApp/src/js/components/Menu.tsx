import React, { useState, useEffect } from 'react';
import { Button, Grid, Typography, BottomNavigation, BottomNavigationAction, Paper } from '@mui/material';
import PhotoCameraIcon from '@mui/icons-material/PhotoCamera';
import PhotoLibraryIcon from '@mui/icons-material/PhotoLibrary';
import HistoryIcon from '@mui/icons-material/History';
import SettingsIcon from '@mui/icons-material/Settings';
import HomeIcon from '@mui/icons-material/Home';
import Settings from './Settings';

const Menu: React.FC = ({ handleImageCaptioning, handleImageUpload, handleCheckCaptions, setModel, model }) => {

    const [value, setValue] = useState('Home');

    return (<div>
            {value === 'Home' && <Grid
                    container
                    spacing={2}
                    direction="column"
                    justifyContent="center"
                    alignItems="center"
                    style={{ minHeight: '100vh' }}
                >
                    <Grid item>
                        <Typography variant="h4" align="center" gutterBottom>
                            Welcome :)
                        </Typography>
                    </Grid>
                    <Grid item>
                        <Button
                            variant="contained"
                            color="primary"
                            startIcon={<PhotoCameraIcon />}
                            onClick={handleImageCaptioning}
                        >
                            Caption with Camera
                        </Button>
                    </Grid>
                    <Grid item>
                        <Button
                            variant="contained"
                            color="primary"
                            startIcon={<PhotoLibraryIcon />}
                            onClick={handleImageUpload}
                        >
                            Caption from Gallery
                        </Button>
                    </Grid>
                    <Grid item>
                        <Button
                            variant="contained"
                            color="secondary"
                            startIcon={<HistoryIcon />}
                            onClick={handleCheckCaptions}
                        >
                            Check Previous Captions
                        </Button>
                    </Grid>
            </Grid>}
        {value === 'Settings' && <Settings setModel={setModel} model={model} /> }
            <Paper sx={{ position: 'fixed', bottom: 0, left: 0, right: 0 }} elevation={3}>
                <BottomNavigation
                    showLabels
                    value={value}
                    onChange={(event, newValue) => {
                        setValue(newValue);
                    }}
                >
                    <BottomNavigationAction label='Home' value='Home'  icon={<HomeIcon/>}/>
                    <BottomNavigationAction label='Settings' value='Settings' icon={<SettingsIcon/>}/>
                </BottomNavigation>
            </Paper>
        </div>
    );
};

export default Menu;