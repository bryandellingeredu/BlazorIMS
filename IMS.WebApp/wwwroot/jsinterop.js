function preventFormSubmission(formId) {
    var form = document.getElementById(formId);
    if (form) {
        form.addEventListener('keydown', function (event) {
            // Check if the pressed key is Enter  
            if (event.key === 'Enter') {
                // Prevent the default form submission
                event.preventDefault();
                return false;
            }   
        });
    }
}  

function setFocus(elementId){
    const element = document.getElementById(elementId);
    if (element) {
        element.focus();
    }
};