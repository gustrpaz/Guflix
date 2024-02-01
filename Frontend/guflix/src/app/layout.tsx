import type { Metadata } from 'next'
import { Inter } from 'next/font/google'
import { Header } from '../components/header'
import './globals.css'

const inter = Inter({ subsets: ['latin'] })

export const metadata: Metadata = {
  title: 'Guflix',
  description: 'Um projeto criado com React, Next e TypeScript',
}

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="pt-br">
      <body className={inter.className}>
        <Header />
        <div className='wrapper'>
          {children}
        </div>
        <footer>
          Gustavo Rezende todos os direitos reservados ®
        </footer>
      </body>
    </html>
  )
}
