# ChatApp

Entity Framework Core ve PostgreSQL kullanılarak geliştirilmiş basit bir sohbet (chat) uygulaması. Proje iki parçadan oluşur:

- **ChatApi** — ASP.NET Core Web API (sohbet ve mesaj işlemlerini yöneten backend)
- **ChatConsole** — API'yi tüketen basit bir konsol istemcisi

## Özellikler

- Sohbet (conversation) oluşturma ve listeleme
- Bir sohbete mesaj gönderme ve mesajları kronolojik sırayla listeleme
- Sohbet detayında mesaj sayısı ve katılımcı listesi
- Konsol istemcisinde ~2 saniyelik polling ile yeni mesajların otomatik görünmesi

## Kullanılan Teknolojiler

- .NET / ASP.NET Core Web API
- Entity Framework Core (Code-First)
- Npgsql.EntityFrameworkCore.PostgreSQL
- PostgreSQL

## Proje Yapısı

```
ChatApp/
├── ChatApi/
│   ├── Controllers/        # ConversationController, MessageController
│   ├── Models/              # Conversation, Message (entity sınıfları)
│   ├── DTOs/                 # İstek/yanıt veri modelleri
│   ├── Migrations/          # EF Core migration dosyaları
│   ├── ChatDbContext.cs     # EF Core DbContext
│   └── Program.cs
├── ChatConsole/
│   └── Program.cs           # HttpClient ile API'ye bağlanan konsol istemcisi
```

## Kurulum

### 1. Gereksinimler
- .NET SDK
- PostgreSQL (yerel veya uzak bir sunucu)

### 2. Veritabanı bağlantısı

ChatApi/appsettings.json kısmına kendi veritabanı şifrenizi girin.


### 3. Veritabanını oluşturma

```bash
cd ChatApi
dotnet ef database update
```

### 4. Çalıştırma

API:
```bash
cd ChatApi
dotnet run
```

Konsol istemcisi (API çalışırken ayrı bir terminalde):
```bash
cd ChatConsole
dotnet run
```

> Konsol istemcisi varsayılan olarak `http://localhost:5273` adresine bağlanır; API'nizin adresi farklıysa `ChatConsole/Program.cs` içindeki `BaseAddress` değerini güncelleyin.

## API Uç Noktaları

| Metot | Yol | Açıklama |
|---|---|---|
| POST | `/api/conversation` | Yeni sohbet oluşturur |
| GET | `/api/conversation` | Tüm sohbetleri listeler |
| GET | `/api/conversation/{id}` | Sohbet detayını getirir (mesaj sayısı, katılımcılar) |
| POST | `/api/conversation/{conversationId}/messages` | Sohbete mesaj ekler |
| GET | `/api/conversation/{conversationId}/messages` | Sohbetteki mesajları listeler |

## Notlar

Bu proje bir staj çalışması kapsamında, Entity Framework Core ve ASP.NET Core Web API temellerini uygulamalı olarak öğrenmek amacıyla geliştirilmiştir.
