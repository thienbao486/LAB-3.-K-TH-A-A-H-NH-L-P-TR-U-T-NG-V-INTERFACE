README - LAB 3 (C# / Visual Studio 2022)

CACH CHAY CHUONG TRINH

1. Mo Visual Studio 2022.
2. Mo project Lab03_OOPAdvanced.
3. Trong Solution Explorer, mo file Program.cs thuoc project.
4. Sao chep toan bo code trong file Lab03_OOPAdvanced_UTF8_Full.cs va dan vao Program.cs, thay the code cu.
5. Dam bao project chi co mot ham Main() va khong co file .cs khac khai bao trung cac lop trong code.
6. Nhan Ctrl + S de luu.
7. Chon Build > Rebuild Solution de bien dich.
8. Nhan Ctrl + F5 de chay chuong trinh.
9. Khi menu hien ra, nhap so tu 1 den 10 de chay bai tuong ung.
10. Nhap 0 de thoat chuong trinh.

UTF-8 VA TIENG VIET CO DAU

Trong ham Main(), chuong trinh da dat:
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

File code duoc luu bang UTF-8 co BOM. Neu tieng Viet van hien thi sai, dong cua so Console cu va chay lai.

NEU GAP LOI

- Loi CS5001: kiem tra Program.cs co ham static void Main() hay khong.
- Loi trung lop: xoa/doi ten cac file .cs cu khai bao trung lop.
- Khong chay duoc: chon Build > Rebuild Solution, doc loi trong Error List va sua loi truoc khi chay.

GHI CHU

Day la chuong trinh Console voi du lieu mau tao trong code. Chuong trinh khong luu du lieu vao co so du lieu.
