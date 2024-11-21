import './App.css'
import ManageSessionsPage from "./Pages/ManageSessionsPage.jsx";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import ManageMoviePage from "./Pages/ManageMoviePage.jsx";

function App() {
  return (
    <>
        <BrowserRouter>
            <Routes>
                <Route path="/">
                    <Route index element={<ManageSessionsPage />} />
                    <Route path="/create/movie" element={<ManageMoviePage />} />
                    {/*<Route path="/edit/film" element={<ManageFilmPage />} />*/}
                </Route>
            </Routes>
        </BrowserRouter>
    </>
  );
}

export default App
