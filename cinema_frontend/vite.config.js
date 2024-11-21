import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '/api': {
        target: 'http://127.0.0.1:5054', // Backend server
        changeOrigin: true,              // Ensure the Host header matches the target
        // rewrite: (path) => path.replace(/^\/api/, ''), // Remove `/api` prefix if needed
        configure: (proxy) => {
          proxy.on('proxyReq', (proxyReq, req, res) => {
            console.log('Proxying request to:', proxyReq.getHeader('host') + proxyReq.path);
          });
        },
      },
    },
  },
});
