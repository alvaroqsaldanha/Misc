import React from 'react';
import { CircularProgress, Grid } from '@mui/material';
import Box from '@mui/material/Box';

const Loading: React.FC = () => {
    return (
        <Grid
                container
                spacing={2}
                direction="column"
                justifyContent="center"
                alignItems="center"
                style={{ minHeight: '100vh' }}
            >
            <Box sx={{ display: 'block' }}>
                <CircularProgress color="secondary" />
            </Box>
            <br></br>
            <h2>Loading...</h2>
        </Grid>
    );
};

export default Loading;