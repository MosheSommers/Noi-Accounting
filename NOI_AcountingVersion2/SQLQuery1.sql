--delete table company;
-- delete table my_transaction
create table company(
	company_id int identity(1, 1),
	name varchar(60) not null,

	constraint pk_company_company_id primary key (company_id)
);

create table my_transaction(
	transaction_id int identity(1, 1),
	transaction_type bit not null,
	transaction_name varchar(60),
	amount money not null,
	date date,
	company_id int not null,

	constraint pk_my_transaction_transaction_id primary key (transaction_id),
	constraint fk_my_transaction_company foreign key(company_id) references company(company_id)
	);