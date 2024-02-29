import React from 'react';
import { Typography, Button } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';

const CaptionedImage = ({ imageCaption, onClose }) => {
    return (
        <div style={{ height: '100vh', display: 'flex', flexDirection: 'column', justifyContent: 'center', alignItems: 'center' }}>
            <Button style={{ position: 'absolute', top: 16, left: 4, fontSize: '6rem', color: '#1976D2' }} onClick={onClose}>
                <CloseIcon />
            </Button>
            <img
                src={imageCaption.image}
                alt="Captioned Image"
                style={{ maxWidth: '100%', maxHeight: '70vh', borderRadius: '8px' }}
            />
            <Typography variant="subtitle1" align="center" style={{ marginTop: '16px', marginBottom: '16px', color: '#616161' }}>
                {imageCaption.caption}
            </Typography>
        </div>
    );
};

export default CaptionedImage;