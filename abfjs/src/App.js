import AbfProject from "./Components/v4/AbfProject";
import TinyLabel from "./Components/v4/TinyLabel";

function App() {
  return (
    <>
      <AbfProject />

      <footer className='mt-5'>
        <TinyLabel text="FOOTER" />
        <div className='p-1'>
          <a href='http://192.168.1.9/abf-browser/api/v4/' style={{ textDecoration: 'none' }}>API Version 4</a>
        </div>
      </footer>
    </>
  );
}

export default App;
