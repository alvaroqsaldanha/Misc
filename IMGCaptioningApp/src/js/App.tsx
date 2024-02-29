import React, { useState } from 'react';
import { Modal, Box, Typography } from '@mui/material';
import { Camera, CameraResultType, CameraSource } from '@capacitor/camera';
import CaptionedImage from './components/CaptionedImage';
import Loading from './components/Loading';
import Menu from './components/Menu';
import fetchImageCaption from './APIHelpers'
import { decode } from "base64-arraybuffer";
import useSQLiteDB from './useSQLiteDB';
import CaptionHistory from './components/CaptionHistory';
import { useDarkMode } from './components/DarkModeContext'


const App: React.FC = () => {

    interface ImageCaption {
        image: '';
        caption: '';
    }

    const [selectedImageCaption, setSelectedImageCaption] = useState<ImageCaption>({ caption: '', image: '' } as ImageCaption);
    const [model, setModel] = useState<string>('https://api-inference.huggingface.co/models/Salesforce/blip-image-captioning-large');  
    const [isCaptioning, setIsCaptioning] = useState(false);
    const [checkingHistory, setCheckingHistory] = useState(false);
    const [error, setError] = useState(false);
    const { performSQLAction } = useSQLiteDB();
    const { darkMode } = useDarkMode();  

    const handleImageCaptioning = async (selectedImage) => {
        setIsCaptioning(true); 
        try {
            const result = await fetchImageCaption(selectedImage,model);
            return result;
        } catch (error) {
            console.error('Error captioning image:', error);
            setError(true);
        }
    };

    const handleImageCapture = async () => {
        const image = await Camera.getPhoto({
            quality: 90,
            allowEditing: false,
            resultType: CameraResultType.Base64,
            source: CameraSource.Camera
        })
        const blob = new Blob([new Uint8Array(decode(image.base64String))], {
            type: `image/${image.format}`,
        });
        const result = await handleImageCaptioning(blob);
        await performSQLAction(async (db) => {
            await db?.query(`INSERT INTO pastCaptions(caption, image, format) VALUES('${result[0]['generated_text']}', '${image.base64String}', '${image.format}');`);
        }, null);
        await setSelectedImageCaption({ caption: result[0]['generated_text'], image: URL.createObjectURL(blob) }); 

    };

    const handleImageUpload = async () => {
        const image = await Camera.getPhoto({
            quality: 90,
            allowEditing: false,
            resultType: CameraResultType.Base64,
            source: CameraSource.Photos
        })
        const blob = new Blob([new Uint8Array(decode(image.base64String))], {
            type: `image/${image.format}`,
        });
        const result = await handleImageCaptioning(blob); 
        await performSQLAction(async (db) => {
            await db?.query(`INSERT INTO pastCaptions(caption, image, format) VALUES('${result[0]['generated_text']}', '${image.base64String}','${image.format}');`);
        }, null);
        await setSelectedImageCaption({ caption: result[0]['generated_text'], image: URL.createObjectURL(blob) }); 
    };

    const handleCheckCaptions = () => {
        setCheckingHistory(true);
    };

    const handleClose = async () => {
        await setSelectedImageCaption({ caption: '', image: '' } as ImageCaption);
        await setIsCaptioning(false);
        await setCheckingHistory(false);
        await setError(false);
    };

    return (
        <div className={darkMode ? 'app-container dark' : 'app-container light'}>
            {!isCaptioning && !checkingHistory ?
                <Menu handleImageCaptioning={handleImageCapture} handleImageUpload={handleImageUpload} handleCheckCaptions={handleCheckCaptions} setModel={setModel} model={model} />
                :
                (!isCaptioning && checkingHistory) ? <CaptionHistory onClose={handleClose} performSQLAction={performSQLAction} /> : null
            }
            {isCaptioning && selectedImageCaption.caption === '' && <Loading />}
            {isCaptioning && selectedImageCaption.caption !== '' && <CaptionedImage onClose={handleClose} imageCaption={selectedImageCaption} />}
            <Modal
                open={error}
                onClose={handleClose}
                aria-labelledby="modal-modal-title"
                aria-describedby="modal-modal-description"
                style={{ display: 'flex', alignItems: 'center', justifyContent: 'center' }}
            >
                <Box sx={{ backgroundColor: 'white', padding: '20px', borderRadius: '8px' }}>
                    <Typography variant="subtitle1" align="center" style={{ marginTop: '16px', color: '#616161' }}>
                        Error captioning image, the API might not be available. Please try again later or select another model.
                    </Typography>
                </Box>
            </Modal>
        </div>
    );
};

export default App;