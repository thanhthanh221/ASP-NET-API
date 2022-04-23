import logo from './logo.svg';
import './App.css';
import { useEffect, useState } from 'react';
import { useMemo } from 'react';
import { useRef } from 'react';

function SetUpAvatar() {    
    const [avatar, setAvatar] = useState();
    const handlePreviewAvatar = (e) => {
        const file = e.target.files[0];

        file.preview = URL.createObjectURL(file);

        setAvatar(file);
    }
    useEffect(() => {
        // Xóa
        return () => {
            avatar && URL.revokeObjectURL(avatar.preview);
        }
    }, [avatar]);
    return (
        <div className="App">
            <input  type="file"
                    onChange={handlePreviewAvatar} />
            {
                avatar &&( <img src={avatar.preview} alt="" width="80%"/>)
            }
        </div>
    );
}
export default SetUpAvatar;