import { useReducer } from "react";

const initState = {
    job: "",
    jobs: []
}
const Set_Job = "set_job";
const Add_job = "add_job";
const Delete_job = "Delete_job";

const reduce = (state, action) => {
    
}

function Test() {
    // Khởi tạo là initState - reduce là hàm xử lý
    const [state, dispatch] = useReducer(reduce, initState);  
    const {job, jobs} = state;

    return (
        <div className="Test">
            <h3>Todo</h3>
            <input placeholder="Nhấn công việc......" value={job} />
            <button>Add</button>
            <ul>
                {
                    jobs.map(job => (
                        <li>{job} &times;</li>
                    ))
                }
            </ul>
        </div>
    )
}
export default Test