/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
    "./node_modules/react-tailwindcss-datepicker/dist/index.esm.js",
  ],
  theme: {
    extend: {
      colors: {
        primary: '#3498db',
        secondary: '#2F2B2B',
        background: '#221F1F',
        text: '#333333',
        textbox: '#554F4F',
      },
    },
  },
  plugins: [],
}