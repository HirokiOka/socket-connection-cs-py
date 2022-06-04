import socket

# サーバ (C#側) とクライアント (Python側) のIP, ポート情報
server_addr = ('127.0.0.1', 3000)
client_addr = ('127.0.0.1', 3001)

# クライアント側のソケット作成 & IP, ポートを指定
client = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
client.bind(client_addr)

while True:
    send_message = input('input message:') # コマンドラインから送りたい文字列を入力
    if send_message != 'end':
        client.sendto(send_message.encode('utf-8'), server_addr) # サーバに文字列を送信する
        msg, addr = client.recvfrom(1024) # サーバからの返事を待って受け取る
        msg = msg.decode(encoding='utf-8')
        print(f'received: {msg}') # サーバから受信した文字列を出力する
client.close()
