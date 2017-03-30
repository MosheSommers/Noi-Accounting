-- drop table my_transaction
--drop table user_info
--drop table company;

create table company(
	company_id int identity(1, 1),
	name varchar(60) not null unique,
	password char(8) not null,

	constraint pk_company_company_id primary key (company_id)
);

create table user_info(
	user_id int identity(1, 1),
	user_name varchar(30) unique,
	password char(8) not null,
	company_id int,

	constraint pk_user_info_user_id primary key (user_id),
	constraint fk_user_info_company foreign key (company_id) references company(company_id)
);

create table my_transaction(
	transaction_id int identity(1, 1),
	transaction_type bit not null,
	transaction_name varchar(60),
	amount money not null,
	date date,
	company_id int not null,
	user_id int,

	constraint pk_my_transaction_transaction_id primary key (transaction_id),
	constraint fk_my_transaction_company foreign key(company_id) references company(company_id),
	constraint fk_my_transaction_user_info foreign key (user_id) references user_info(user_id)
	);
	
