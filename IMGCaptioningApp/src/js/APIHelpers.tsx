const API_TOKEN = '';

const fetchImageCaption = async (imageUri, model) => {
    try {
        const response = await fetch(
            model,
            {
                headers: { Authorization: `Bearer ${API_TOKEN}` },
                method: "POST",
                body: imageUri,
            }
        );
        if (response.status !== 200) {
            throw "Incorrect request."
        }
        const result = await response.json();
        return result;
    } catch (error) {
        console.error('Error fetching image caption:', error);
        throw error;
    }
};

export default fetchImageCaption;