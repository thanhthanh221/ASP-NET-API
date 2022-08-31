import { createSlice } from "@reduxjs/toolkit";
const jwtLogin = localStorage.getItem('Jwt') === null ? null : localStorage.getItem('Jwt');

const initialState = {
  jwt: jwtLogin,
};

export const loginProject = createSlice({
  name: "LoginProject",
  initialState,
  reducers: {
    setJwt: (state, action) => {
      state.jwt = action.payload;
    },
    removeJwt: (state, action) => {
      state.jwt = action.payload;
    },
  },
});

export const { setJwt, removeJwt } = loginProject.actions;

export default loginProject.reducer;
