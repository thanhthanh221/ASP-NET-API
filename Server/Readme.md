# Back End ASP NET(C#)

## Run Sever :Dotnet Run
1. khởi tạo MongoDb(máy đã có Docker)
- docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo
2. Mô Hình Domain Driven Design -(Khai triển 3 Layer)
- API(Application Layer) [Tầng Truyền dữ liệu -> Client]
- Infrastructure Layer   [Tầng cơ sở (CSDL - ORM - DOCKER )]
- Domain Layer           [Tầng nền Thao tác nghiệp vụ]