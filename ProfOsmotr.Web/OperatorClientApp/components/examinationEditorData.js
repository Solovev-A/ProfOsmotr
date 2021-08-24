import React from 'react';

const ExaminationEditorData = ({ editor }) => {
    return (
        <p className='font-italic'>
            {'Последние изменения: '}
            {editor.fullName}
            {editor.position ? ' ,' : ''}
            {editor.position}
        </p>
    )
}

export default ExaminationEditorData;